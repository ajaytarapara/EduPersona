import AdminPage from "../../pages/AdminPage";
import ProfilePage from "../../pages/Profile";
import { Roles, AppRoutes } from "../../utils";

export const ProtectedRoutesConfig = [
  {
    path: AppRoutes.Profile,
    element: <ProfilePage />,
    allowRoles: [Roles.USER],
  },
  {
    path: AppRoutes.Admin,
    element: <AdminPage />,
    allowRoles: [Roles.ADMIN],
  },
];
