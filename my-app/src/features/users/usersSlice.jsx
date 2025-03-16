
import { createSlice, createAsyncThunk } from "@reduxjs/toolkit";
 import axios from "axios";


// // הבאת רשימת משתמשים
// יצירת פעולה אסינכרונית כדי להביא את הנתונים מה-API
// יצירת פעולה אסינכרונית כדי להביא את הנתונים מה-API
export const fetchUsers = createAsyncThunk(
  'users/fetchUsers',
  async () => {
    const response = await fetch('http://localhost:7206/api/Client');
    const data = await response.json();
    return data; // מחזיר את הנתונים
  }
);




// הוספת משתמש חדש
export const addUser = createAsyncThunk("users/addUser", async (newUser) => {
  return { ...newUser, id: Math.floor(Math.random() * 1000) };
});

// עדכון משתמש
export const updateUser = createAsyncThunk("users/updateUser", async (user) => {
  return user;
});

// התחברות משתמש
export const loginServer = createAsyncThunk("user-login", async (user, thunkAPI) => {
  try {
    const { data } = await axios.post("https://reqres.in/api/login", {
      email: user.email,
      password: user.password
    });
    return data; // ה-API מחזיר טוקן במקרה של הצלחה
  } catch (error) {
    return thunkAPI.rejectWithValue(error.response?.data?.error || "התחברות נכשלה");
  }
});

// הרשמת משתמש
export const registerServer = createAsyncThunk(
  "users/register",
  async (user, thunkAPI) => {
    try {
      const { data } = await axios.post("https://reqres.in/api/register", {
        email: user.email,
        password: user.password,
      });
      return data;
    } catch (error) {
      return thunkAPI.rejectWithValue(error.response?.data?.error || "הרשמה נכשלה");
    }
  }
);

// מחיקת משתמש
export const deleteUser = createAsyncThunk("users/deleteUser", async (id) => {
  return id;
});

const usersSlice = createSlice({
  name: 'users',
  initialState: {
    users: [],
    status: 'idle', // או 'loading', 'succeeded', 'failed'
    error: null,
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchUsers.pending, (state) => {
        state.status = 'loading';
      })
      .addCase(fetchUsers.fulfilled, (state, action) => {
        state.status = 'succeeded';
        state.users = action.payload;
      })
      .addCase(fetchUsers.rejected, (state, action) => {
        state.status = 'failed';
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
      .addCase(registerServer.pending, (state) => {
        state.status = "loading";
      })
      .addCase(registerServer.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.currentUser = action.payload; // נרשם בהצלחה
      })
      .addCase(registerServer.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload;
      })
      .addCase(loginServer.pending, (state) => {
        state.status = "loading";
        state.error = null;
      })
      .addCase(loginServer.fulfilled, (state, action) => {
        state.status = "succeeded";
        state.currentUser = action.payload; // התחברות בהצלחה
      })
      .addCase(loginServer.rejected, (state, action) => {
        state.status = "failed";
        state.error = action.payload;
      });
  }
});

export default usersSlice.reducer;
