import React, { Component } from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faChevronLeft, faChevronRight, faChevronUp, faChevronDown } from '@fortawesome/free-solid-svg-icons'

export class Robot extends Component {

    render () {

        const {
            direction 
        } = this.props;

        switch (direction) {
            case 'North':
                return (<FontAwesomeIcon icon={faChevronUp} />);
            case 'East':
                return (<FontAwesomeIcon icon={faChevronRight} />);
            case 'South':
                return (<FontAwesomeIcon icon={faChevronDown} />);
            case 'West':
                return (<FontAwesomeIcon icon={faChevronLeft} />);
        }
    }

}