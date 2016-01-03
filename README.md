# auto-startup
This is a auto startups management service for windows.

You can configure applications what you want to start as soon as auto-startups starts.
As a windows service, it will starting to run when your pc powered, without logging in.

# Install
- Download msi file from [here](https://github.com/NeilQ/auto-startup/releases) and install it.
- Configure applications what you want to start at .config file in your installed folder.
    ```
     <autoStartup>
         <startups>
             <add name="ipsync" path="d:/ipsync/ipsync.exe" args="-f f:/"/>
             <add name="ipconfig" path="ipconfig" args=""/>
         </startups>
     </autoStartup>
    ```
    > **name**, just a unique name of one record.
    > **path**, the application path what you want to auto-start. Or an command name like "ipconfig".
    > **args**, the command args needed.

- You can manage the service manually, which names "auto-startup".



### TODOS
- [x] argument support 
- [x] add logger
- [x] add installer
- [x] package release installer
- [x] enrich readme
- [ ] multi threads support  # no so much performance requirement
- [ ] clear dead process    # same with above
