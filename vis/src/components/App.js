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
          <Link to="visualization">Visualizaiton</Link>
        </header>
        <div className="container">
          <RouteHandler />
        </div>
      </div>
    );
  }
});

module.exports = App;
