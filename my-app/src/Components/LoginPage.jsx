import React, { useEffect } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { fetchUsers } from '../features/users/usersSlice';

const LoginPage = () => {
  const dispatch = useDispatch();
  const { users, status, error } = useSelector((state) => state.users);

  useEffect(() => {
    const fetchData = async () => {
      try {
        // שליחת הבקשה דרך Redux
        await dispatch(fetchUsers());
      } catch (err) {
        console.error('שגיאה:', err);
      }
    };

    fetchData();
  }, [dispatch]);

  return (
    <div className="login-page">
      <h1>הלקוחות שלי</h1>
      {status === 'loading' && <p>טוען...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <ul>
        {users.map((user) => (
          <li key={user.id}>{user.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default LoginPage;






// import React, { useEffect, useState } from 'react';
// import { useDispatch, useSelector } from 'react-redux';
// import { useNavigate } from 'react-router-dom';
// import { createSelector } from '@reduxjs/toolkit';
// //  import { fetchUsers, loginServer } from '../features/users/usersSlice';
//  import { fetchUsers} from '../features/users/usersSlice';


// // יצירת selector מותאם כדי למשוך נתונים מ-Redux
// const selectAuth = createSelector(
//   (state) => state.users || { status: '', error: '' },
//   (users) => users
// );

//  const LoginPage = async () => {
// //   const dispatch = useDispatch();
// //   const navigate = useNavigate();

// const dispatch = useDispatch();

// useEffect(() => {
//   const fetchData = async () => {
//     try {
//       const users = await dispatch(fetchUsers()).unwrap(); // שימוש ב-unwrap לקבלת הנתונים ישירות
//       console.log("בדיקה", users);
//     } catch (error) {
//       console.log('שגיאה', error);
//     }
//   };
  
//   fetchData();
// }, [dispatch]); // הוספת dispatch כתלות





//   const [username, setUsername] = useState('');
//   const [password, setPassword] = useState('');
//   const [email, setEmail] = useState('');

//   const { status, error, user } = useSelector(selectAuth);

//   // const Message = "הכנס את פרטי המשתמש";

//   // const handleSubmit = async (e) => {
//   //   e.preventDefault();
//   //   const userData = { username, password, email };

//   //   // שליחת בקשת התחברות לשרת דרך Redux
//   //   const resultAction = await dispatch(loginServer(userData));

//   //   console.log("Result Action:", resultAction); // 🔍 בדיקה: מה מחזיר ה-Redux

//   //   if (loginServer.fulfilled.match(resultAction)) {
//   //     const token = resultAction.payload?.token;
//   //     if (token) {
//   //       localStorage.setItem("token", token);
//   //       console.log("Login successful! Token:", token);
//   //       navigate('/home');
//   //     }
//   //   } else {
//   //     console.error("Login failed:", resultAction.error?.message || "Unknown error");
//   //   }
//   // };

//   return (
    
//     <div className="login-page">
//       {/* <h2>התחברות</h2>
//       <h1>{Message}</h1>
//       <form onSubmit={handleSubmit}>
//         <div>
//           <label>שם משתמש:</label>
//           <input
//             type="text"
//             value={username}
//             onChange={(e) => setUsername(e.target.value)}
//             placeholder="הכנס שם משתמש"
//             required
//           />
//         </div>
//         <div>
//           <label>סיסמה:</label>
//           <input
//             type="password"
//             value={password}
//             onChange={(e) => setPassword(e.target.value)}
//             placeholder="הכנס סיסמה"
//             required
//           />
//         </div>
//         <div>
//           <label>מייל:</label>
//           <input
//             type="email"
//             value={email}
//             onChange={(e) => setEmail(e.target.value)}
//             placeholder="הכנס מייל"
//             required
//           />
//         </div>
//         {status === 'loading' && <p>...המתן</p>}
//         {error && <p style={{ color: 'red' }}>{error}</p>}
//         <button type="submit" disabled={status === 'loading'}>התחבר</button>
//       </form> */}
//     </div>
//   );
// };

// export default LoginPage;
