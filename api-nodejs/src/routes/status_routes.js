import express from "express";
import {getAllStatuses} from '../controllers/status_controller.js'

const router = express.Router();

router.get("/", getAllStatuses);

export default router;