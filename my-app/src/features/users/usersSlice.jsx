import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";
import { useSelector } from "react-redux";

// נתונים מדגמיים
const mockUsers = [
  { id: 1, name: "User 1" },
  { id: 2, name: "User 2" },
  { id: 3, name: "User 3" },
];

// הבאת רשימת משתמשים - עכשיו מחזיר נתונים מדגמיים
export const fetchUsers = createAsyncThunk("users/fetchUsers", async () => {
  // החזר נתונים מדגמיים במקום קריאה ל-API
  return mockUsers;
});

// הוספת משתמש חדש - לא משנה את הקריאה ל-API, מכיוון שהיא עדיין לא מחוברת
export const addUser = createAsyncThunk("users/addUser", async (newUser) => {
  // בעת חיבור ל-API, יש להחזיר את הקריאה ל-API כאן
  // const response = await axios.post(API_URL, newUser);
  // return response.data;
  
  // כרגע מחזיר משתמש מדגמיות
  return { ...newUser, id: Math.floor(Math.random() * 1000) }; // הוספת id באופן אקראי
});

// עדכון משתמש
export const updateUser = createAsyncThunk("users/updateUser", async (user) => {
  // בעת חיבור ל-API, יש להחזיר את הקריאה ל-API כאן
  // const response = await axios.put(`${API_URL}/${user.id}`, user);
  // return response.data;

  // כרגע מחזיר את המשתמש כפי שהוא
  return user;
});
//הרשמת משתמש
// שינוי שם הפונקציה ל-loginServer
export const loginServer = createAsyncThunk("user-login", async (user, thunkApi) => {
  const state = thunkApi.getState();
  let message = state.user.message;
  let { data } = await axios.post("login", user);
  return data;
});

//התחברות משתמש

export const rejisterSrever=createAsyncThunk("user-rejister",async(user,tunkApi)=>{
  let message=useSelector(s=>s.user.meassenge)
let {data}=await axios.post("adduser",user);
return data

})
// מחיקת משתמש
export const deleteUser = createAsyncThunk("users/deleteUser", async (id) => {
  // בעת חיבור ל-API, יש להחזיר את הקריאה ל-API כאן
  // await axios.delete(`${API_URL}/${id}`);
  
  // כרגע מחזיר את ה-id כדי להסיר אותו מתוך ה-state
  return id;
});

const usersSlice = createSlice({
  name: "users",
  initialState: {
    users: [],
    status: "idle", // idle | loading | succeeded | failed
    message:"",
    error: null,
  },
  reducers: {
    login: (state, action) => {
      state.currentUser = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchUsers.pending, (state) => {
        state.status = "loading";
      })
      .addCase(fetchUsers.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.users = action.payload;
      })
      .addCase(fetchUsers.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.error.message;
      })
      .addCase(addUser.fulfilled, (state, action) => {
        state.users.push(action.payload);
      })
      .addCase(updateUser.fulfilled, (state, action) => {
        const index = state.users.findIndex((user) => user.id === action.payload.id);
        if (index !== -1) {
          state.users[index] = action.payload;
        }
      })
      .addCase(deleteUser.fulfilled, (state, action) => {
        state.users = state.users.filter((user) => user.id !== action.payload);
      })
      .addCase(loginServer.fulfilled, (state, action) => { 

          state.currentUser=action.payload
      })
      .addCase(loginServer.rejected, (state, action) => {
        state.status="failed"
  })
  .addCase(loginServer.pending, (state, action) => {   
     state.message=" בהתחברות קרתה תקלה"
    state.status="pending"
})
.addCase(rejisterSrever.fulfilled,(state,action)=>{
  state.currentUser=action.payload.id;
})
.addCase(rejisterSrever.rejected, (state, action) => {
  state.status="failed"
})
  },
});
export const {login} =usersSlice.actions
export default usersSlice.reducer;
