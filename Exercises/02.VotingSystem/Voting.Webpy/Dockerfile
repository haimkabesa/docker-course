FROM    python:3 

RUN 	pip install requests
RUN     pip install flask 

EXPOSE  5000 

ADD     . /app
ENTRYPOINT ["python"]
CMD     ["./app/main.py" ]