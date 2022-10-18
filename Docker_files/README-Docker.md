docker build -f Dockerfile_BE -t remotefinder_be .
docker build -f Dockerfile_UI -t remotefinder_ui .

docker login --username=adryanbarbe --email=adryanbarbe@gmail.com

docker login -u username -p password registry.hub.docker.com/myproject
docker stack deploy --compose-file docker-compose.yml remotefinder --with-registry-auth
