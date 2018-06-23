FROM    python:3 
LABEL Maintainer="haimk@codevalue.net"

RUN     pip install redis
RUN 	pip install requests
RUN     pip install flask 

EXPOSE  5000 

ADD     . /app
ENTRYPOINT ["python"]
CMD     ["./app/main.py" ]