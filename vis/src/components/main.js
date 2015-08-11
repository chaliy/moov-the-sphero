'use strict';

var React = require('react');
var Router = require('react-router');
var Route = Router.Route;


var App = require('./App');
var Visualization = require('./Visualization');

var Routes = (
  <Route handler={App} path="/">
    <Route name="visualization" handler={Visualization}/>
  </Route>
);

Router.run(Routes, function (Handler) {
  React.render(<Handler/>, document.body);
});
