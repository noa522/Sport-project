import { useState } from 'react';
import './App.css';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';  // ודא שאתה מייבא את Routes

import { Provider } from 'react-redux';
import { store } from './app/store';
import Users from './components/Users';
import LoginPage from './components/LoginPage'; // שים לב לשם הקומפוננטה!

function App() {
  const isLoggedIn = false; // מצב זה יכול לבוא מתוך Redux או useState אם רוצים

  return (
    <Provider store={store}>
      <Router>
        <Routes>
          {/* אם המשתמש לא מחובר, מציגים את דף ההתחברות */}
          {!isLoggedIn ? (
            <Route path="/" element={<LoginPage />} />
          ) : (
            // אם המשתמש מחובר, מציגים את דף המשתמשים
            <Route path="/" element={<Users />} />
          )}
        </Routes>
      </Router>
    </Provider>
  );
}

export default App;