'use strict';

describe('VisApp', () => {
  let React = require('react/addons');
  let VisApp, component;

  beforeEach(() => {
    let container = document.createElement('div');
    container.id = 'content';
    document.body.appendChild(container);

    VisApp = require('components/VisApp.js');
    component = React.createElement(VisApp);
  });

  it('should create a new instance of VisApp', () => {
    expect(component).toBeDefined();
  });
});
