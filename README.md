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

## Running on Raspeberry Pi
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

## Running on Docker
1. Open terminal on the root of the solution
2. Build the image
````command
docker-compose build
````
3. Run the container
````command
docker-compose up -d
````
### Limitations when running on Docker
* Available operations:
````graphql
query Q {
  raspberryPi {
    chipset {
      model
      revision
      serial
      version
    }
    cpu {
      cores
      model
      temperature
      averageLoad {
        lastMinute
        last5Minutes
        last15Minutes
      }
      realTimeLoad {
        kernel
        user
        total
      }
    }
    disk {
      fileSystem
      type
      total
      used
      available
    }
    memory {
      total
      used
      available
    }
    gpu {
      memory
    }
    os {
      name
      kernel
      hostname
    }
  }
}
````
