import express from "express";
import { getAllUsers, getUserById, createUser } from "../controllers/user_controller.js";

const router = express.Router();

// Rutas de usuarios
router.post("/", createUser);               // POST /api/usuarios - crear usuario
router.get("/", getAllUsers);               // GET /api/usuarios - obtener todos
router.get("/:id", getUserById);            // GET /api/usuarios/:id - obtener por ID

export default router;
