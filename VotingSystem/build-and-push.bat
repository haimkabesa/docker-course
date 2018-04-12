docker build Voting.Webpy -t localhost:5000/voting-web
docker push localhost:5000/voting-web

docker build Voting.Processor -t localhost:5000/voting-processor
docker push localhost:5000/voting-processor

docker build Voting.API -t localhost:5000/voting-api
docker push localhost:5000/voting-api