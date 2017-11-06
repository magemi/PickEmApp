import * as React from 'react';
import { RouteComponentProps } from 'react-router';

export class Register extends React.Component<RouteComponentProps<{}>, {}> {
  public render() {
    return <div className="vertical-center">
      <form>
        <div className="form-group">
          {/*<label htmlFor="registerEmailInput">Email</label>*/}
          <input type="email" className="form-control" id="registerEmailInput" placeholder="Email" />
        </div>

        <div className="form-group">
          {/*<label htmlFor="registerPasswordInput">Password</label>*/}
          <input type="password" className="form-control" id="registerPasswordInput" placeholder="Password" />
        </div>

        <div className="form-group">
          {/*<label htmlFor="registerConfirmPasswordInput">Confirm Password</label>*/}
          <input type="password" className="form-control" id="registerConfirmPasswordInput" placeholder=" Confirm Password" />
        </div>

        <input type="button" className="form-control btn btn-primary" id="registerButtonInput" value="Sign Up" />
      </form>
    </div>;
  }
}