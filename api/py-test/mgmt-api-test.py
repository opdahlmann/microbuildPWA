### VARIABLES -->

projectId = "5c9dbe2dc0b3621390345096"
templateId = '_'
workorderId = "5c9dbedac0b36213903450a0"
workorderDoorId = "5ca6e26da3a55f3758519832"

### TOKEN -->

token = 'D9swnylCy2sfd_U9RA7b8KBrX5VaCKHBFI023HTjjXa5CQOj7yqvgKiII8da2pzhOmegJ5l65kXEGMomRU6FHjzOHF2VF1eSwGFGSJJ0T4hjXItkpiNB9o9gVxkD_Bc2kwyQfJpEpstYXRCtkroz0_rxJRK_vnVU99O-HUihFQlIuXnQGZFH6GTZVIVKDofxyRx6e49ZMVC4SUDW1ZviDtMMnsjXy6IQ0CUcQIzZDPyWP03wZIRw8eSy2kHH-yKA'

### API URL -->

BASE_URL = 'http://localhost:59659/'
  
### ==================================================

import requests
import json

PARAMS = { 'Authorization': 'Bearer ' + token } 

def _get_params():
	token = login()
	return { 'Authorization': 'Bearer ' + token }

def _json(r):
	x = r
	try:
		x = json.dumps(r.json(), indent=4)
	except Exception as e:
		pass
	return x

### ==================================================
  
def login():
	func = 'login'
	data = { "UserName": "nirmall@embla.asia", "Password": "peter" }
	params = { 'Content-Type': 'text/plain', 'Accept': 'application/json' } 
	r = requests.post(url = BASE_URL + func, params = params, data = data)
	# print(r.json())
	return r.json()
print(login())

def projects_admin():
	func = 'projects'
	r = requests.get(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# projects_admin()

def projects_mobile():
	func = 'projects/view/mobile'
	r = requests.get(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# projects_mobile()

def templates():
	func = 'projects/' + projectId + '/templates'
	r = requests.get(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# templates()

def workorders():
	func = 'projects/' + projectId + '/templates/' + templateId + '/workorders'
	r = requests.get(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# workorders()

def start_workorder():
	func = 'projects/{}/templates/{}/workorders/{}/start'.format(projectId, templateId, workorderId)
	r = requests.post(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# start_workorder()

def sync_preview():
	func = 'projects/'+projectId+'/sync/preview'
	r = requests.get(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# sync_preview()

def message_image():
	func = 'projects/'+projectId+'/doors/messages/'+messageId+'/view/image'
	r = requests.get(url = BASE_URL + func, headers = PARAMS)
	print(_json(r))
# message_image()

def notes():
	func = "projects/"+projectId+"/templates/"+templateId+"/workorders/"+workorderId+"/doors/"+workorderDoorId+"/notes"
	r = requests.get(url = BASE_URL + func, headers = _get_params())
	print(_json(r))
# notes()

# def maintain_hardware():
# 	func = "workorders/"+workorderId+"/doors/"+doorId+"/hardware/maintain"
# 	data = { "": "" } # TODO
# 	r = requests.get(url = BASE_URL + func, params = params, headers = _get_params(), data = data)
# 	print(_json(r))
# maintain_hardware()

