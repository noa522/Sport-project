import React, { useState, useEffect } from "react";
import { Box, Typography, Card, CardMedia, CardContent, Button, TextField, IconButton, Dialog, DialogActions, DialogContent, DialogTitle } from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import "./Exercise.css"

const Exercise = ({ exercise, onNext, onClose }) => {
  const [score, setScore] = useState("");
  const [timeLeft, setTimeLeft] = useState(exercise.time * 60);
  const [isTimeUp, setIsTimeUp] = useState(false);
  const [openDialog, setOpenDialog] = useState(false);

  useEffect(() => {
    setScore("");
    setTimeLeft(exercise.time * 60);
    setIsTimeUp(false);
  }, [exercise]);

  useEffect(() => {
    if (timeLeft > 0) {
      const timer = setInterval(() => {
        setTimeLeft((prevTime) => prevTime - 1);
      }, 1000);
      return () => clearInterval(timer);
    } else {
      setIsTimeUp(true);
      setOpenDialog(true);
    }
  }, [timeLeft]);

  const handleScoreChange = (e) => {
    const value = Math.max(1, Math.min(10, Number(e.target.value)));
    setScore(value);
  };

  const handleNext = () => {
    onNext(score);
  };

  const handleCloseDialog = (response) => {
    if (response) {
      handleNext();
    }
    setOpenDialog(false);
  };

  const isValidScore = Number.isInteger(score) && score >= 1 && score <= 10;

  return (
    <Box
      sx={{
        maxWidth: "100vw",
        minHeight: "100vh",
        backgroundColor: "#f9f9f9",
        display: "flex",
        flexDirection: "column",
        justifyContent: "center",
        alignItems: "center",
        textAlign: "center",
        position: "relative",
        overflow: "hidden",
        padding: 0,
      }}
    >
      <IconButton
        onClick={onClose}
        sx={{
          position: "absolute",
          top: 10,
          left: 10,
          color: "#ff5722",
          zIndex: 1,
        }}
      >
        <CloseIcon fontSize="large" />
      </IconButton>

      <Typography
        variant="h3"
        fontWeight="bold"
        color="#ff5722"
        sx={{
          marginBottom: 3,
          zIndex: 2,
          position: "relative",
          fontSize: { xs: "2.5rem", sm: "3.5rem" },
          textAlign: "center",
        }}
      >
        {exercise.name}
      </Typography>

      <Card sx={{ boxShadow: "none", backgroundColor: "transparent", display: "flex", justifyContent: "center", marginBottom: 3 }}>
        <CardMedia
          component="img"
          image={exercise.image}
          alt={exercise.name}
          sx={{
            borderRadius: "15px",
            height: "50vh",
            objectFit: "contain", 
            width: "90vw",
            marginBottom: 3,
          }}
        />
      </Card>

      {/* הצגת הזמן כתור טקסט גדול בצבע צהוב */}
      <Typography
        variant="h2"
        sx={{
          color: "yellow",
          fontWeight: "bold",
          fontSize: "4rem",
          marginBottom: 3,
        }}
      >
        {Math.floor(timeLeft / 60)}:{String(timeLeft % 60).padStart(2, "0")}
      </Typography>

      <CardContent sx={{ position: "relative", maxWidth: "600px", width: "90%", marginTop: 3 }}>
        <Typography variant="body1" color="text.secondary" sx={{ mb: 2, fontSize: "1.1rem", lineHeight: 1.5 }}>
          {exercise.description}
        </Typography>

        <TextField
          label="הזן ציון (1-10)"
          variant="outlined"
          type="number"
          fullWidth
          value={score}
          onChange={handleScoreChange}
          inputProps={{ min: 1, max: 10 }}
          sx={{
            mb: 2,
            backgroundColor: "white",
            borderRadius: 1,
            boxShadow: "0px 2px 10px rgba(0, 0, 0, 0.1)",
            padding: 2,
          }}
        />

        <Button
          variant="contained"
          sx={{
            width: "100%",
            backgroundColor: "#ff5722",
            color: "white",
            fontSize: "1.2rem",
            fontWeight: "bold",
            "&:hover": {
              backgroundColor: "#d84315",
            },
            padding: "15px",
            fontSize: "1.5rem",
            marginTop: 2,
            position: "relative", // מוודא שהכפתור קרוב יותר לציון
          }}
          onClick={handleNext}
          disabled={!isValidScore || isTimeUp}
        >
          תרגיל הבא
        </Button>
      </CardContent>

      <Dialog open={openDialog} onClose={() => setOpenDialog(false)}>
        <DialogTitle>הזמן עבר!</DialogTitle>
        <DialogContent>
          <Typography variant="body1">האם ברצונך לעבור לתרגיל הבא?</Typography>
        </DialogContent>
        <DialogActions>
          <Button onClick={() => handleCloseDialog(false)} color="secondary">
            לא
          </Button>
          <Button onClick={() => handleCloseDialog(true)} color="primary">
            כן
          </Button>
        </DialogActions>
      </Dialog>
    </Box>
  );
};

export default Exercise;
