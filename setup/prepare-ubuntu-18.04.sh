https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/
wget -q https://packages.microsoft.com/config/ubuntu/18.04/prod.list 
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list
sudo apt-get install apt-transport-https -y
sudo apt-get update 
sudo apt-get install dotnet-sdk-2.2 -y
sudo apt-get install docker.io -y
sudo apt-get install docker-compose -y
