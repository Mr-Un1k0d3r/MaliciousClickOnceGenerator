# ClickOnceGenerator
Quick Malicious ClickOnceGenerator for Red Team

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

```

`config.json` example. The shellcode payload.bin need to be a `RAW` format of your shellcode
```
{
        "title": "My Evil ClickOnce",
        "url": "http://ringzer0team.com/",
        "shellcode": "payload.bin",
        "process_name": "iexplore"
}
```
# Credit
Mr.Un1k0d3r RingZer0 Team
