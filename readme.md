# Mongo Command
docker run --name some-mongo -p 27017:27017 -v mongovolume:/data/db -v mongodbconfig:/data/configdb -d mongo