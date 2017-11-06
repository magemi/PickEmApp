import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Login extends React.Component<RouteComponentProps<{}>, {}> {
  public render() {
    return <div className="vertical-center">
      <form>
        <div className="form-group">
          <input type="email" className="form-control" id="loginEmailInput" placeholder="Email" />
        </div>

        <div className="form-group">
          <input type="password" className="form-control" id="loginPasswordInput" placeholder="Password" />
        </div>

        <input type="button" className="form-control btn btn-primary" id="loginButtonInput" value="Sign In" />
      </form>
    </div>;
  }
}