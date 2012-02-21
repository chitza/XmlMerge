SET XPATH=Configuration/RemoteObjects/RemoteObject
XmlMerge -s src.xml -m "%XPATH%" -t tgt.xml -n "%XPATH%[last()]" -o out.xml
pause