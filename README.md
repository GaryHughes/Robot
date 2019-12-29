# Robot #

![](https://github.com/GaryHughes/Robot/workflows/Robot%20Build/badge.svg)

![](https://github.com/GaryHughes/Robot/workflows/Robot%20Test/badge.svg)

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

## Notes ##

* In the interests of simplicity there is no persistence in the backend.
* Each HTTP session has it's own Robot instance.

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

## Deployment ##

### Docker ###



### Kubernetes ###