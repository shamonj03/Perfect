# Mongo Command
docker run --rm --name some-mongo -p 27017:27017 -v mongovolume:/data/db -v mongodbconfig:/data/configdb -d mongo

# Rabbit COmmand
docker run --rm --hostname my-rabbit --name some-rabbit -p 15672:15672 -p 5672:5672 -v rabbitvolume:/var/lib/rabbitmq -d rabbitmq:3-management