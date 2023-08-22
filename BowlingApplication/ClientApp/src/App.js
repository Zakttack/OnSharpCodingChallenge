import React, { Component } from 'react';
import './custom.css';
import Participant from './components/Participant';

export default class App extends Component {

  render() {
    return (
      <div>
        <h1>Welecome To Bowling</h1>
        <Participant/>
      </div>
    )
  }
}
