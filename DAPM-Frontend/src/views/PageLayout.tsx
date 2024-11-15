import Header from '../components/headers/Header.tsx';

interface PageLayoutProps {
    user: any;
    children?: React.ReactNode;
    }
    
const PageLayout: React.FC<PageLayoutProps> = ({ user, children }) => {
    
    return (
        <>
            <Header userInfo={user}/>
            {children}
        </>
    )
}

export default PageLayout;