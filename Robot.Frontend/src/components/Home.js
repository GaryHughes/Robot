import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faUndo, faRedo, faWalking } from '@fortawesome/free-solid-svg-icons'
import * as api from './RobotApi'
import { Robot } from './Robot'
import './Home.css'

export class Home extends Component {

  constructor(props) {
    
    super(props);

    this.state = {
      width: null,
      height: null,
      x: null,
      y: null,
      direction: null  
    };

    this.performAction = this.performAction.bind(this);
  }

  async performAction(action) {

    const response = await action();

    // TODO
    //console.log(response);

    const data = await response.json();
    
    this.setState(data);
}

  async componentDidMount() {
    await this.performAction(async () => await api.world());  
  }

  render () {
   
    const {
      width,
      height,
      x,
      y,
      direction
    } = this.state;

    if (!width || !height) {
      return (<div>Loading...</div>)  
    }

    const robotHasBeenPlaced = x >= 0 && y >= 0 && direction;
   
    const rows = height;
    const cols = width;
   
    return (
      <div className="grid">
      <br></br>
        {
          Array.from({ length: rows }, (value, row) => { 
            let rowKey = `${row}`;
            return (
              <div className="row" key={rowKey}>
              {
                Array.from({ length: cols }, (value, col) => {
                  let key = `${row}=${col}`;
                  return (
                    <div className="box" key={key}>
                      <div className="inner" onClick={() => this.performAction(async () => await api.place(col, row, 'North'))}>
                        { robotHasBeenPlaced && x === col && y === row && <Robot direction={direction}/> }
                    </div>
                    </div>
                  );
                })
              }
              </div>
            )
          }).reverse()
        }
        <br></br>
        <div className="row">
          <div className="box"><div className="inner" title="Turn Left" onClick={() => this.performAction(async () => await api.turnLeft())}><FontAwesomeIcon icon={faUndo} /></div></div>
          <div className="box"><div className="inner" title="Turn Right" onClick={() => this.performAction(async () => await api.turnRight())}><FontAwesomeIcon icon={faRedo} /></div></div>
          <div className="box"><div className="inner" title="Move Forward" onClick={() => this.performAction(async () => await api.move())}><FontAwesomeIcon icon={faWalking} /></div></div>
        </div>
      </div>
    );
  }
}
