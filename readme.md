# Mongo Command
docker run --rm --name some-mongo -p 27017:27017 -v mongovolume:/data/db -v mongodbconfig:/data/configdb -d mongo

# Rabbit Command
docker run --rm --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 -v rabbitvolume:/var/lib/rabbitmq -d rabbitmq:3-management

# Azurite Command
docker run --rm --name some-azurite -p 10000:10000 -d mcr.microsoft.com/azure-storage/azurite azurite-blob --blobHost 0.0.0.0 --blobPort 10000