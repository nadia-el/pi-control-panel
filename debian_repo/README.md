# Debian Package Repository 

Debian ARM packages hosted on rembertmagri/pi-control-panel github repository.
Package list:
* [pi-control-panel](https://github.com/rembertmagri/pi-control-panel)

**To add this repository to your list:**

1. Add to the Apt sources:
````bash
wget -qO - https://raw.githubusercontent.com/rembertmagri/pi-control-panel/master/debian_repo/PUBLIC.KEY | sudo apt-key add -
echo 'deb https://raw.githubusercontent.com/rembertmagri/pi-control-panel/master/debian_repo buster main' | sudo tee -a /etc/apt/sources.list
sudo apt-get update
````

2. Install a package
````bash
sudo apt-get install pi-control-panel
````

# Development

## Update the Repository with new Package Version
1. Download the new versions of the package and include them into the repository
````bash
export GPG_TTY=$(tty)
dpkg-sig --sign builder --gpg-options "--batch --pinentry-mode loopback --passphrase THE_KEY_PASSPHRASE" ../pi-control-panel_1.11_arm*
reprepro -S utils includedeb buster ../pi-control-panel_1.11_arm*.deb
````

2. Check that the packages have been added to the repository
````bash
reprepro list buster
````

## Installing a Debian Package Repository from scratch
1. Install required libraries
````bash
sudo apt-get install gnupg dpkg-sig reprepro
````

2. Generate key
````bash
gpg --gen-key
````
or import existing key
````bash
gpg --allow-secret-key-import --import PRIVATE.KEY
````

3. Get the key id to be added as 'SignWith' property of the Distributions file
![KEY_ID](https://user-images.githubusercontent.com/30979154/83982061-51bac200-a8f1-11ea-9809-ef31b16c31f5.png)

4. Create repository
````bash
mkdir debian
cd debian
mkdir conf
touch conf/distributions
````

5. Distributions file content
````
Origin: Rembert Magri
Label: Rembert Magri
Suite: stable
Codename: buster
Architectures: armhf arm64
Components: main
Description: Debian ARM packages hosted on rembertmagri/debian github repository
SignWith: F4109F88DD8DCD60
````

6. Copy the public key to the folder
````bash
gpg --output PUBLIC.KEY --armor --export rembert.magri@gmail.com
````

7. Update the repository with the packages
````bash
reprepro -S utils includedeb buster ../packageName_version_architecture.deb
````
