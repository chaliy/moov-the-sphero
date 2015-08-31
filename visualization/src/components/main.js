'use strict';

// Stores
var eventsStore = require('../stores/eventsStore');
eventsStore.start();

var React = require('react');
var Router = require('react-router');
var Route = Router.Route;


var App = require('./App');
var Events = require('./Events');
var Position = require('./Position');

var Routes = (
  <Route handler={App} path='/'>
    <Route name='events' path='events' handler={Events}/>
    <Route name='position' path='position' handler={Position}/>
  </Route>
);

Router.run(Routes, function (Handler) {
  React.render(<Handler/>, document.body);
});
