import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faChevronLeft, faChevronRight, faChevronUp, faChevronDown, faUndo, faRedo, faWalking } from '@fortawesome/free-solid-svg-icons'
import * as api from './RobotApi'
import './Home.css'

export class Home extends Component {

  render () {
    const rows = 5;
    const cols = 5;
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
                      <div className="inner" onClick={e => Robot.place(row, col)}>
                        
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
          <div className="box"><div className="inner" title="Face West" onClick={api.faceWest}><FontAwesomeIcon icon={faChevronLeft} /></div></div>
          <div className="box"><div className="inner" title="Face North" onClick={api.faceNorth}><FontAwesomeIcon icon={faChevronUp} /></div></div>
          <div className="box"><div className="inner" title="Face East" onClick={api.faceEast}><FontAwesomeIcon icon={faChevronRight} /></div></div>
          <div className="box"><div className="inner" title="Face South" onClick={api.faceSouth}><FontAwesomeIcon icon={faChevronDown} /></div></div>
          <div className="box"><div className="inner" title="Turn Left" onClick={api.turnLeft}><FontAwesomeIcon icon={faUndo} /></div></div>
          <div className="box"><div className="inner" title="Turn Right" onClick={api.turnRight}><FontAwesomeIcon icon={faRedo} /></div></div>
          <div className="box"><div className="inner" title="Move Forward" onClick={api.move}><FontAwesomeIcon icon={faWalking} /></div></div>
        </div>
      </div>
    );
  }
}
