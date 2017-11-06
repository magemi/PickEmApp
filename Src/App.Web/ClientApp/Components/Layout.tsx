import * as React from 'react';
import { NavMenu } from './NavMenu';

export interface LayoutProps {
    children?: React.ReactNode;
}

export class Layout extends React.Component<LayoutProps, {}> {
    public render() {
      return <div className='container-fluid'>
            <div className='row'>
              <NavMenu />
            </div>
            <div className='row'>
                <div className='col-sm-4'></div>
                <div className='col-sm-4'>
                    { this.props.children }
                </div>
                <div className='col-sm-4'></div>
            </div>
        </div>;
    }
}
