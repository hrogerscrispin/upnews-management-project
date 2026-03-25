import { category } from "../models/category_schema.js";

// Crear una nueva categoría
// export const createCategory = async (req, res) => {
//   try {
//     const { nombreCategoria, descripcion } = req.body;

//     if (!nombreCategoria || !descripcion) {
//       return res.status(400).json({
//         success: false,
//         message: 'Los campos nombreCategoria y descripción son requeridos'
//       });
//     }

//     const nuevaCategoria = new category({
//       nombreCategoria,
//       descripcion
//     });

//     await nuevaCategoria.save();

//     res.status(201).json({
//       success: true,
//       message: 'Categoría creada correctamente',
//       data: nuevaCategoria
//     });
//   } catch (error) {
//     console.error('Error al crear categoría:', error);
//     res.status(500).json({
//       success: false,
//       message: 'Error al crear categoría',
//       error: error.message
//     });
//   }
// };

// Obtener todas las categorías
export const getAllCategories = async (req, res) => {
  try {
    const categorias = await category.find();

    res.status(200).json({
      success: true,
      message: 'Categorías obtenidas correctamente',
      data: categorias
    });
  } catch (error) {
    console.error('Error al obtener categorías:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener categorías',
      error: error.message
    });
  }
};

// Obtener categoría por ID
export const getCategoryById = async (req, res) => {
  try {
    const { id } = req.params;
    const cat = await category.findById(id);

    if (!cat) {
      return res.status(404).json({
        success: false,
        message: 'Categoría no encontrada'
      });
    }

    res.status(200).json({
      success: true,
      message: 'Categoría obtenida correctamente',
      data: cat
    });
  } catch (error) {
    console.error('Error al obtener categoría:', error);
    res.status(500).json({
      success: false,
      message: 'Error al obtener categoría',
      error: error.message
    });
  }
};
