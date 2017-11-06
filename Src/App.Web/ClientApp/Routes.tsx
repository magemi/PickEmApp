import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './Components/Layout';
import { Home } from './Components/Home';
import { Register } from './Components/Account/Register';
import { Login } from './Components/Account/Login';

export const routes = <Layout>
  <Route exact path='/' component={Home} />
  <Route exact path='/register' component={Register} />
  <Route exact path='/login' component={Login} />
</Layout>;