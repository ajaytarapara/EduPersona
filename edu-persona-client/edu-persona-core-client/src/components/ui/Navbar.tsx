import React, { useState } from "react";
import { Link, useLocation, useNavigate } from "react-router-dom";
import {
  AppBar,
  Toolbar,
  IconButton,
  Typography,
  Button,
  Menu,
  MenuItem,
  Avatar,
  Drawer,
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Box,
  Divider,
  useTheme,
} from "@mui/material";

import {
  Menu as MenuIcon,
  Home,
  History,
  Dashboard,
  Person,
  Settings,
  Logout,
  EmojiEvents,
  KeyboardArrowDown,
} from "@mui/icons-material";

import { ToggleTheme } from "./ToggleTheme";

const navLinks = [
  { label: "Home", path: "/", icon: <Home /> },
  { label: "History", path: "/history", icon: <History /> },
  { label: "Dashboard", path: "/dashboard", icon: <Dashboard /> },
  { label: "User Matrix", path: "/matrix", icon: <EmojiEvents /> },
  { label: "User Profile", path: "/profile", icon: <Person /> },
  { label: "Settings", path: "/settings", icon: <Settings /> },
];

export const Navbar = () => {
  const [mobileOpen, setMobileOpen] = useState(false);
  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const location = useLocation();
  const navigate = useNavigate();
  const theme = useTheme();

  const isActive = (path: string) => location.pathname === path;

  const handleLogout = () => {
    navigate("/");
  };

  return (
    <>
      <AppBar
        position="sticky"
        elevation={0}
        sx={{
          bgcolor: theme.palette.background.default,
          color: theme.palette.text.primary,
          borderBottom: `1px solid ${theme.palette.divider}`,
        }}
      >
        <Toolbar sx={{ justifyContent: "space-between", minHeight: 64 }}>
          {/* LEFT LOGO */}
          <Box display="flex" alignItems="center" gap={1}>
            <IconButton
              sx={{ display: { md: "none" } }}
              onClick={() => setMobileOpen(true)}
            >
              <MenuIcon />
            </IconButton>

            <Typography
              component={Link}
              to="/"
              sx={{
                fontWeight: 700,
                fontSize: 20,
                textDecoration: "none",
                color: theme.palette.primary.main,
              }}
            >
              Edupersona
            </Typography>
          </Box>

          {/* CENTER NAV */}
          <Box sx={{ display: { xs: "none", md: "flex" }, gap: 1 }}>
            {navLinks.slice(0, 5).map((link) => {
              const active = isActive(link.path);
              return (
                <Button
                  key={link.path}
                  component={Link}
                  to={link.path}
                  startIcon={link.icon}
                  sx={{
                    textTransform: "none",
                    px: 2,
                    borderRadius: 999,
                    fontWeight: 500,
                    bgcolor: active
                      ? theme.palette.primary.light
                      : "transparent",
                    color: active
                      ? theme.palette.primary.main
                      : theme.palette.text.secondary,
                    "&:hover": {
                      bgcolor: theme.palette.primary.light,
                    },
                  }}
                >
                  {link.label}
                </Button>
              );
            })}
          </Box>

          {/* RIGHT */}
          <Box display="flex" alignItems="center" gap={1}>
            <ToggleTheme />

            <Button
              onClick={(e) => setAnchorEl(e.currentTarget)}
              endIcon={<KeyboardArrowDown />}
              sx={{
                textTransform: "none",
                color: theme.palette.text.primary,
              }}
            >
              <Avatar
                sx={{
                  bgcolor: theme.palette.primary.main,
                  width: 32,
                  height: 32,
                  mr: 1,
                  fontSize: 14,
                }}
              >
                DU
              </Avatar>
              Demo User
            </Button>

            <Menu
              anchorEl={anchorEl}
              open={Boolean(anchorEl)}
              onClose={() => setAnchorEl(null)}
              PaperProps={{ sx: { width: 180, mt: 1 } }}
            >
              <MenuItem component={Link} to="/profile">
                <Person fontSize="small" sx={{ mr: 1 }} />
                Profile
              </MenuItem>
              <MenuItem component={Link} to="/settings">
                <Settings fontSize="small" sx={{ mr: 1 }} />
                Settings
              </MenuItem>
              <Divider />
              <MenuItem
                onClick={handleLogout}
                sx={{ color: theme.palette.error.main }}
              >
                <Logout fontSize="small" sx={{ mr: 1 }} />
                Logout
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </AppBar>

      {/* MOBILE DRAWER */}
      <Drawer open={mobileOpen} onClose={() => setMobileOpen(false)}>
        <Box width={280} p={2}>
          <Typography
            fontWeight={700}
            mb={2}
            color={theme.palette.primary.main}
          >
            Edupersona
          </Typography>

          <List>
            {navLinks.map((link) => {
              const active = isActive(link.path);
              return (
                <ListItemButton
                  key={link.path}
                  component={Link}
                  to={link.path}
                  onClick={() => setMobileOpen(false)}
                  sx={{
                    borderRadius: 2,
                    mb: 0.5,
                    bgcolor: active
                      ? theme.palette.primary.light
                      : "transparent",
                  }}
                >
                  <ListItemIcon sx={{ color: theme.palette.primary.main }}>
                    {link.icon}
                  </ListItemIcon>
                  <ListItemText primary={link.label} />
                </ListItemButton>
              );
            })}

            <Divider sx={{ my: 2 }} />

            <ListItemButton onClick={handleLogout}>
              <ListItemIcon>
                <Logout color="error" />
              </ListItemIcon>
              <ListItemText
                primary="Logout"
                sx={{ color: theme.palette.error.main }}
              />
            </ListItemButton>
          </List>
        </Box>
      </Drawer>
    </>
  );
};
