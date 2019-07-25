# Pwa

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 8.1.1.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).


#how to build and run
App needs to be run over https. To expose the app through https:// surge is used.

1. use https://surge.sh and install surge.

2. Go to project folder and open cmd

3. Run `surge`. It will show the project. 

4. press Enter. It will provide a domain (ex:-`gullible-achieve.surge.sh`)

5. press Enter. project will upload to the given domain.

6. Change the API_BASE in the enviroment.prod.ts to public url provided from the ngrok (ex:- `https://146d77ee.ngrok.io`)

7. Run `ng build --prod`

8. Run `cd .\dist\pwa` 

9. Run `surge ./ [domain]`(ex:- `surge ./ gullible-achieve.surge.sh`)

10. search `https://[domain]` in the browser (ex:- `https:// gullible-achieve.surge.sh`).you will see the app you designed.




