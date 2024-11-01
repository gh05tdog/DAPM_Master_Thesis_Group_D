// Spinner.js
import React from 'react';
import '../CSS/Spinner.css'; // Make sure to import your CSS file

const Spinner = () => {
  return (
    <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
        <div className="loader"></div>
    </div>
  );
};

export default Spinner;