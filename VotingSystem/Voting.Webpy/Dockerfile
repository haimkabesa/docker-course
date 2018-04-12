FROM    python:3 
# tiangolo/uwsgi-nginx-flask:python3.6

RUN     pip install redis
RUN 	pip install requests
RUN     pip install flask 

EXPOSE  5000 

ADD     . /app
ENTRYPOINT ["python"]
CMD     ["./app/main.py" ]