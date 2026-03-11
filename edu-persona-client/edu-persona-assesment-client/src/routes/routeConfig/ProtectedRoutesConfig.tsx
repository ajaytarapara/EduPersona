import { lazy } from "react";
import { Roles, AppRoutes } from "../../utils";

const DashboardPage = lazy(() => import("../../pages/Dashboard"));
const ExamHistoryPage = lazy(() => import("../../pages/ExamHistory"));

export const ProtectedRoutesConfig = [
  {
    path: AppRoutes.Dashboard,
    element: <DashboardPage />,
    allowRoles: [Roles.USER],
  },
  {
    path: AppRoutes.ExamHistory,
    element: <ExamHistoryPage />,
    allowRoles: [Roles.ADMIN],
  },
];
