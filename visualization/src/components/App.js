'use strict';

var React = require('react');
var Router = require('react-router');
var Link = Router.Link;
var RouteHandler = Router.RouteHandler;

// CSS
require('normalize.css');
require('../styles/main.css');

var App = React.createClass({
  render: function() {
    return (
      <div>
        <header>
          <ul>
            <li><Link to='events'>Events</Link></li>
            <li><Link to='position'>Position</Link></li>
          </ul>
        </header>
        <div className='container'>
          <RouteHandler />
        </div>
      </div>
    );
  }
});

module.exports = App;
