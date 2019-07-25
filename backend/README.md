# microbuildPWA
project implemented using two approaches.

1. using Lib.Net.http.webpush - currently the implementation related to this library is commented out.Currently this is sending notifications

2.using webpush - currently this is not sending notifications

# how to buld and run.
App needs to be run over https. so ngrock is used to get public urls to expose local server.

1. install ngrok in machine(`https://ngrok.com/)

2. build and run the app in visual studio

3. open power shell and run `ngrok http 59622 -host-header="localhost:59622"`

4. Now done with backend part.

5. Put provided https://.....................ngrok.io as the API_BASE in frontend and build front end.
(ex:-`https://90626fa0.ngrok.io` )

# Issues at the current state
1. Not supporting safari
2. Browser inconsistencies
3. Issues in multiple subscription. (When having no longer valid subscriptions)
4. library issues.
