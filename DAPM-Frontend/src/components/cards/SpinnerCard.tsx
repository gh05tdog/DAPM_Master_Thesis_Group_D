import React from 'react';
import spinnerSvg from '../../assets/gear-spinner.svg';

interface SpinnerProps {
  size?: number;
  speed?: string;
}

const Spinner: React.FC<SpinnerProps> = ({ size = 75, speed = '4s' }) => (
  <div
    style={{
      width: size,
      height: size,
      display: 'inline-block',
      animation: `spin ${speed} linear infinite`,
    }}
  >
    <img
      src={spinnerSvg}
      alt="Loading spinner"
      style={{
        width: '100%',
        height: '100%',
      }}
    />
    <style>
      {`
        @keyframes spin {
          0% { transform: rotate(0deg); }
          100% { transform: rotate(360deg); }
        }
      `}
    </style>
  </div>
);

export default Spinner;
