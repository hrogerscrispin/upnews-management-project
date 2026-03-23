import express from "express";
import { getAllCategories, getCategoryById } from "../controllers/category_controller.js";

const router = express.Router();

// router.post("/", createCategory);           // POST /api/categorias - crear categoría
router.get("/", getAllCategories);          // GET /api/categorias - obtener todas
router.get("/:id", getCategoryById);        // GET /api/categorias/:id - obtener por ID

export default router;
