import React, {Component} from 'react';

export default class Participant extends Component{
    constructor(props) {
        super(props);
        this.state = {
            playername: ''
        };
    }

    addPlayer() {
        fetch ('/api/Player/AddPlayer', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body:
                JSON.stringify({name: this.state.playername })
        });
    }

    handleChange = (event) => {
        this.setState({playername: event.target.value});
    }

    render() {
        return (
            <div>
                <p>Enter Player: <input type='text' value={this.state.playername} onChange={this.handleChange}/></p>
                <button type='button' onClick={this.addPlayer}>Add Player</button>
            </div>
        )
    }


}