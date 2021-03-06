# Robot #

This workflow builds the project, runs the tests, builds the docker containers, and pushes them to the Google Container Registry.

![](https://github.com/GaryHughes/Robot/workflows/Robot%20Build/badge.svg)

## Overview ##

Robot is a simple application that allows the user to direct a robot around a world described by a grid.

The robot can be placed in any square on the grid, it can turn left or right, and it can move one space at a time in the direction it is currently facing. Any attempts to move beyond the bounds of the grid will be ignored.

The application is comprised of the following components.

| Component | Description |
|-----------|-------------|
|Robot| The underlying robot library |
|Robot.Cli| A command line driver for the robot library |
|Robot.Frontend| An [ASP.Net Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) application that serves a [React](https://reactjs.org) frontend that uses the Robot.Backend |
|Robot.Backend| An [ASP.Net Core](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-3.1) application that provides an API that drives the Robot library for the Robot.Frontend |

<img src="robot_screenshot.png" width="606" height="1182">

## Notes ##

* In the interests of simplicity there is no persistence in the backend.
* Each HTTP session has it's own Robot instance.
* Due to the lack of persistence the API usage stats are per instance.

## Configuration ##

The dimensions of the grid the robot navigates are configured in Robot.Backend/robot.json

``` json
{
    "Robot": {
        "World": {
            "Width": 5,
            "Height": 5
        }
    }
}
```

The listening endpoints for both Robot.Backend and Robot.Frontend can be configured in hosting.json in the respective projects.

``` json
{
  "urls": "http://*:8080"
}
```

When running local processes for developmemnt the following proxy value in package.json needs to match the hosting.json file in Robot.Backend to avoid CORS errors.

``` json
},
  "proxy": "http://localhost:8090"
}
```

## Deployment ##

.env.production contains the following variable. 

REACT_APP_ROBOT_API_ENDPOINT=http://&lt;ENDPOINT&GT;/

ENDPOINT needs to be able to serve both the front and back end applications to avoid CORS errors. For example it could be Google Cloud Ingress that routes /Api/ requests to the backend and anything else to the frontend.

## Google Clould Platform ##

The GCP folder contains yaml exported from a sample deployment. This deployment has a frontend and backend workload, corresponding frontend and backend services, and an ingress that routes to these services.