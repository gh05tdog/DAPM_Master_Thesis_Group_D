import React, { useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { logout } from '../utils/keycloak.ts';

interface LogoutPageProps {
  user: any;
}

const LogoutPage: React.FC<LogoutPageProps> = ({ user }) => {
  const navigate = useNavigate();

  useEffect(() => {
    const performLogout = async () => {
      await new Promise(res => setTimeout(res, 1000));
      await logout();
    };

    performLogout();
  }, [navigate]);

  return (
    <div
      data-qa="LogoutPage" style={{
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        height: '100vh',
        backgroundColor: '#f4f4f9', // Light background color
        fontFamily: 'Arial, sans-serif',
        color: '#333',
        textAlign: 'center'
      }}>
      <div style={{
        padding: '30px',
        borderRadius: '10px',
        backgroundColor: '#fff',
        boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
        textAlign: 'center'
      }}>

        <h1 style={{ fontSize: '2rem', marginBottom: '15px' }}>Logging out...</h1>
        <p style={{ fontSize: '1.2rem', color: '#777' }}>Please wait while we log you out.</p>
      </div>
    </div>
  );
};

export default LogoutPage;