import React from 'react';
import Layout from '../features/layout/Layout';

interface Props {
    children: React.ReactNode,
    showSidebar?: boolean
}

const PublicRoute: React.FC<Props> = ({
    children,
    showSidebar = true
}) => {
    return (
        <Layout showSidebar={showSidebar}>
            {children}
        </Layout>
    )
}

export default PublicRoute;