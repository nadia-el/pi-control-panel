# Pi Control Panel

## Create user to run the app
````bash
sudo useradd -m picontrolpanel
sudo passwd picontrolpanel
sudo usermod -aG sudo picontrolpanel
echo 'picontrolpanel ALL=(ALL) NOPASSWD: ALL' | sudo EDITOR='tee -a' visudo
sudo usermod -aG video picontrolpanel
````

## Running

### Running on Raspberry Pi
1. Publish PiControlPanel.Api.GraphQL project targeting ARM and copy the files to /home/picontrolpanel
2. Navigate to PiControlPanel.Ui.Angular project folder and build the client
````bash
ng build --prod
````
3. Copy the contents of the build (under the dist folder) to /home/picontrolpanel/PiControlPanel.Ui.Angular/dist
4. .Net Core Installation
````bash
mkdir dotnet
wget https://download.visualstudio.microsoft.com/download/pr/da60c9fc-c329-42d6-afaf-b8ef2bbadcf3/14655b5928319349e78da3327874592a/aspnetcore-runtime-3.1.1-linux-arm.tar.gz
sudo mkdir -p /usr/share/dotnet
sudo tar -zxf aspnetcore-runtime-3.1.1-linux-arm.tar.gz -C /usr/share/dotnet/
sudo ln -s /usr/share/dotnet/dotnet /usr/bin/dotnet
````
5. Run as process
````bash
export ASPNETCORE_URLS=http://+:8080
export ASPNETCORE_ENVIRONMENT=Production
dotnet PiControlPanel.Api.GraphQL.dll
````
6. Or run as service
````bash
sudo cp picontrolpanel.service /etc/systemd/system/picontrolpanel.service
sudo chmod 644 /etc/systemd/system/picontrolpanel.service
sudo systemctl enable picontrolpanel
````
7. Access http://<<ip_of_raspberry_pi>>:8080/

#### Available operations
To test the available operations directly, run the application as a process setting the environment to Development and accessing http://<<ip_of_raspberry_pi>>:8080/playground

##### Login Query

Query:
````graphql
query Login($userAccount: UserAccountInputType) {
  login(userAccount: $userAccount)
}
````

Query variables:
````graphql
{
  "userAccount": {
    "username": "pi",
    "password": "raspberry"
  }
}
````

##### RaspberryPi Query

Query:
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
      temperature {
        value
        dateTime
      }
      averageLoad {
        lastMinute
        last5Minutes
        last15Minutes
        dateTime
      }
      realTimeLoad {
        kernel
        user
        total
        dateTime
      }
    }
    disk {
      fileSystem
      type
      total
      status {
        used
        available
        dateTime
      }
    }
    memory {
      total
      status {
        used
      	available
        dateTime
      }
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

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

##### Cpu Subscription

Query:
````graphql
subscription S {
    cpuTemperature {
      value
      dateTime
    }
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

##### Shutdown Mutation
Returns true, but doesn't actually shut the container down.

Query:
````graphql
mutation M {
  shutdown
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

### Running on Docker

#### Docker on Raspberry Pi
````bash
curl -fsSL https://get.docker.com -o get-docker.sh
sh get-docker.sh
rm get-docker.sh
sudo usermod -aG docker picontrolpanel
sudo apt-get install docker-compose
apt-get -y install git
git clone https://github.com/rembertmagri/pi-control-panel.git
cd pi-control-panel/Docker/
docker-compose -f docker-compose.pi.yml build
docker-compose -f docker-compose.pi.yml up -d
````
Access http://<<ip_of_raspberry_pi>>:8081/

#### Docker outside Raspberry Pi
1. Open terminal inside the solution's /Docker directory
2. Build the image
````command
docker-compose build
````
3. Run the container
````command
docker-compose up -d
````
4. Access http://localhost:8080/

#### Available operations when running on Docker

##### Login Query

Query:
````graphql
query Login($userAccount: UserAccountInputType) {
  login(userAccount: $userAccount)
}
````

Query variables:
````graphql
{
  "userAccount": {
    "username": "pi",
    "password": "raspberry"
  }
}
````

##### RaspberryPi Query

Query:
````graphql
query Q {
  raspberryPi {
    disk {
      fileSystem
      type
      total
      status {
        used
        available
        dateTime
      }
    }
    memory {
      total
      status {
        used
      	available
        dateTime
      }
    }
  }
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

##### Shutdown Mutation
Returns true, but doesn't actually shut the container down.

Query:
````graphql
mutation M {
  shutdown
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

## GraphQL Connections

Besides the above mentioned query fields, these are the supported GraphQL connection fields. They return paginated lists and can be used with the other fields in the same request.

### Cpu Temperatures

Query:
````graphql
query Q($firstTemperatures: Int, $afterTemperatures: String) {
  raspberryPi {
    cpu {
      temperatures(first: $firstTemperatures, after: $afterTemperatures) {
        items {
          value
          dateTime
        }
        pageInfo {
          endCursor
          hasNextPage
          startCursor
          hasPreviousPage
        }
        totalCount
      }
    }
  }
}
````

Query variables:
````graphql
{
  "firstTemperatures": 20,
  "afterTemperatures": null
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

### Cpu Average Loads

Query:
````graphql
query Q($firstAverageLoads: Int, $afterAverageLoads: String) {
  raspberryPi {
    cpu {
      averageLoads(first: $firstAverageLoads, after: $afterAverageLoads) {
        items {
          lastMinute
          last5Minutes
          last15Minutes
          dateTime
        }
        pageInfo {
          endCursor
          hasNextPage
          startCursor
          hasPreviousPage
        }
        totalCount
      }
    }
  }
}
````

Query variables:
````graphql
{
  "firstAverageLoads": 20,
  "afterAverageLoads": null
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

### Cpu Real-time Loads

Query:
````graphql
query Q($firstRealTimeLoads: Int, $afterRealTimeLoads: String) {
  raspberryPi {
    cpu {
      realTimeLoads(first: $firstRealTimeLoads, after: $afterRealTimeLoads) {
        items {
          kernel
          user
          total
          dateTime
        }
        pageInfo {
          endCursor
          hasNextPage
          startCursor
          hasPreviousPage
        }
        totalCount
      }
    }
  }
}
````

Query variables:
````graphql
{
  "firstRealTimeLoads": 20,
  "afterRealTimeLoads": null
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

### Disk Statuses

Query:
````graphql
query Q($firstDiskStatuses: Int, $afterDiskStatuses: String) {
  raspberryPi {
    disk {
      status {
        used
        available
        dateTime
      }
      statuses(first: $firstDiskStatuses, after: $afterDiskStatuses) {
	    items {
          used
          available
          dateTime
        }
        pageInfo {
          endCursor
          hasNextPage
          startCursor
          hasPreviousPage
        }
        totalCount
      }
    }
  }
}
````

Query variables:
````graphql
{
  "firstDiskStatuses": 20,
  "afterDiskStatuses": null
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````

### Memory Statuses

Query:
````graphql
query Q($firstMemoryStatuses: Int, $afterMemoryStatuses: String) {
  raspberryPi {
    memory {
      status {
        used
        available
        dateTime
      }
      statuses(first: $firstMemoryStatuses, after: $afterMemoryStatuses) {
	    items {
          used
          available
          dateTime
        }
        pageInfo {
          endCursor
          hasNextPage
          startCursor
          hasPreviousPage
        }
        totalCount
      }
    }
  }
}
````

Query variables:
````graphql
{
  "firstMemoryStatuses": 40,
  "afterMemoryStatuses": null
}
````

HTTP Headers:
````graphql
{
  "Authorization" : "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI..."
}
````
