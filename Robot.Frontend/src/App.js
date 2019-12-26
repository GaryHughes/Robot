import React, { Component } from 'react';
import { Route } from 'react-router';
import { Home } from './components/Home';

import './custom.css'

export default class App extends Component {

  render () {
    return (
      <div>
        <Route exact path='/' component={Home} />
      </div>
    );
  }
  
}
