Management API
==============

## Running Mobile

### ... on DevApp

Change `.vs/config/applicationhost.config`:

- `<site name="MicroBuild.Management.API" ...` 
	- should have the binding path as `<binding protocol="http" bindingInformation="*:59659:" />`
	- ^^ notice the removal of "localhost" from `*:59659:localhost`
- Run Visual Studio as Admin (otherwise this will be blocked).