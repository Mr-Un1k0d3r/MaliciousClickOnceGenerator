# ClickOnceGenerator
Quick Malicious ClickOnceGenerator for Red Team. The default application a simple WebBrowser widget that point to a website of your choice.

# Usage
```
$ python ClickOnceGenerator.py --help


ClickOnceGenerator | Mr.Un1k0d3r RingZer0 Team
usage: ClickOnceGenerator.py [-h] [--config CONFIG] [--out OUT]
                             [--override [OVERRIDE]]

ClickOnceGenerator Options.

optional arguments:
  -h, --help            show this help message and exit
  --config CONFIG       Path to the JSON config file.
  --out OUT             Output solution name.
  --override [OVERRIDE]
                        Delete destination if exists
  --report [REPORT]     Add POST requests with running processes                    

```

```
python ClickOnceGenerator.py --config config.json --out myClickOnce --override True --report True
```

`config.json` example. The shellcode payload.bin need to be the `RAW` format of your shellcode.
```
{
        "title": "My Evil ClickOnce",
        "url": "http://ringzer0team.com/",
        "shellcode": "payload.bin",
        "process_name": "iexplore"
}
```
* `title` is the title of the ClickOnce Application
* `url` url used by the WebBrowser widget
* `shellcode` the payload you want to execute while the application is launched
* `process_name` used to evade sandbox by checking if a specific process is running. (default to `iexplore`)

# Generating the Visual Studio Project

Once the files are created in the target folder create a new C# project and import the files.

To publish the ClickOnce in Visual Studio go to *Project* -> *Publish*

# Credit
Mr.Un1k0d3r RingZer0 Team
