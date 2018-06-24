from flask import Flask, request, render_template
import os
import random
import requests
import socket
import sys

app = Flask(__name__)

local_votes = {}
local_mode='true'
api_address='voting.api'
# Load configurations from environment or config file
app.config.from_pyfile('config_file.cfg')

if ("LOCAL" in os.environ):
    local_mode= os.environ['LOCAL']

if ("API_ADDRESS" in os.environ):
    api_address= os.environ['API_ADDRESS']

if ("VOTE1VALUE" in os.environ and os.environ['VOTE1VALUE']):
    button1 = os.environ['VOTE1VALUE']
else:
    button1 = app.config['VOTE1VALUE']

if ("VOTE2VALUE" in os.environ and os.environ['VOTE2VALUE']):
    button2 = os.environ['VOTE2VALUE']
else:
    button2 = app.config['VOTE2VALUE']

if ("TITLE" in os.environ and os.environ['TITLE']):
    title = os.environ['TITLE']
else:
    title = app.config['TITLE']


# Change title to host name to demo NLB
if app.config['SHOWHOST'] == "true":
    title = socket.gethostname()




@app.route('/', methods=['GET', 'POST'])
def index():

    if request.method == 'GET':

        opt1 = get_votes(button1) 
        opt2 = get_votes(button2) 

        # Get current values
        vote1 = opt1
        vote2 = opt2            

        # Return index with values
        return render_template("index.html", value1=int(vote1), value2=int(vote2), button1=button1, button2=button2, title=title)

    elif request.method == 'POST':

        if request.form['vote'] == 'reset':
                        
            reset()
            vote1 = get_votes(button1) 
            vote2 = get_votes(button2) 
            return render_template("index.html", value1=int(vote1), value2=int(vote2), button1=button1, button2=button2, title=title)
        
        else:

            # Insert vote result into DB
            vote = request.form['vote']
            add_vote(vote)
            opt1 = get_votes(button1) 
            opt2 = get_votes(button2) 
        
            # Get current values
            vote1 = opt1
            vote2 = opt2        

            # Return results
            return render_template("index.html", value1=int(vote1), value2=int(vote2), button1=button1, button2=button2, title=title)


def get_votes(option):
    if option not in local_votes: local_votes[option]=0        
    if local_mode:     
        return local_votes[option];
    else:
        res=requests.get('http://'+api_address+'/api/votes/'+option)
        return res.text

def add_vote(option):
    if option not in local_votes: 
        local_votes[option]=0
    if local_mode:     
        local_votes[option] = local_votes[option]+1
    else:
        requests.post('http://'+api_address+'/api/votes/'+option)

def reset():
    if local_mode:     
        local_votes[button1] = 0
        local_votes[button2] = 0

if __name__ == "__main__":
    #app.run()
    app.run(host='0.0.0.0', port=5000)
