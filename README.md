# pi-control-panel

## .Net Core Installation
````bash
mkdir dotnet
wget https://download.visualstudio.microsoft.com/download/pr/da60c9fc-c329-42d6-afaf-b8ef2bbadcf3/14655b5928319349e78da3327874592a/aspnetcore-runtime-3.1.1-linux-arm.tar.gz
sudo mkdir -p /usr/share/dotnet
sudo tar -zxf aspnetcore-runtime-3.1.1-linux-arm.tar.gz -C /usr/share/dotnet/
sudo ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet
````

## Create user to run the app
````bash
sudo useradd -m picontrolpanel
sudo passwd picontrolpanel
sudo usermod -aG sudo picontrolpanel
echo 'picontrolpanel ALL=(ALL) NOPASSWD: ALL' | sudo EDITOR='tee -a' visudo
sudo usermod -aG video picontrolpane
````

## Running
1. Publish the porject targeting ARM and copy the files to /home/picontrolpanel
2. Run as process
````bash
export ASPNETCORE_URLS=http://+:8080
export ASPNETCORE_ENVIRONMENT=Development
dotnet PiControlPanel.Api.GraphQL.dll
````
3. Or run as service
````bash
sudo cp picontrolpanel.service /etc/systemd/system/picontrolpanel.service
sudo chmod 644 /etc/systemd/system/picontrolpanel.service
sudo systemctl enable picontrolpanel
````
