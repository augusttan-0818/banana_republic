"use client";

import React, { useEffect, useState } from "react";
import {
  AppBar,
  Toolbar,
  Tabs,
  Tab,
  IconButton,
  Menu,
  MenuItem,
  Avatar,
  Button,
  Tooltip,
  Typography,
} from "@mui/material";
import LoginIcon from "@mui/icons-material/Login";
import LogoutIcon from "@mui/icons-material/Logout";
import { useRouter, usePathname } from "next/navigation";
import { useTranslations } from "next-intl";
import GlobalNavDropdown from "./GlobalNavDropdown";
import { useSession, signIn, signOut } from "next-auth/react";
import { ROLES, Role } from "@/app/auth/roles";
import { NavArea } from "../../types/navigation";

const GlobalNavigation: React.FC = () => {
  const router = useRouter();
  const pathname = usePathname();
  const segments = pathname.split("/");
  const locale = segments[1] || "en";
  const t = useTranslations("GlobalNavigation");
  const { data: session } = useSession();
  const isLoggedIn = !!session;
  const userRoles: Role[] = (session?.roles ?? []) as Role[];

  const [value, setValue] = useState(0);
  const [anchorElog, setAnchorElog] = useState<null | HTMLElement>(null);

  const handleAvatarClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorElog(event.currentTarget);
  };

  const handleMenuClose = () => setAnchorElog(null);

  const hasAccess = (requiredRoles?: Role[], requireAll = false): boolean => {
    if (!requiredRoles || requiredRoles.length === 0) return true;
    return requireAll
      ? requiredRoles.every((role) => userRoles.includes(role))
      : requiredRoles.some((role) => userRoles.includes(role));
  };

  const areas: NavArea[] = [
    { label: t("home"), path: `/${locale}/` },
    { label: t("codeVariations"), path: `/${locale}/code-variations` },
    {
      label: "Work Items",
      path: `/${locale}/work-items`,
      tabs: [
        {
          label: t("workPlanning"),
          path: `/${locale}/work-planning`,
          tabs: [
            {
              label: "Committees",
              path: `/${locale}/work-planning/committees`,
            },
            {
              label: "Work Plan",
              path: `/${locale}/work-planning/tasks/work-plan`,
            },
            { label: "Meetings", path: `/${locale}/work-planning/meetings` },
            // TODO: Delete the commented out tabs when the features are fully removed
            //{ label: t('codesTeam'), path: `/${locale}/codes-team` },
            //{ label: "Resource Hours", path: `/${locale}/work-planning/resources` }
          ],
        },
        {
          label: t("referenceDocuments"),
          path: `/${locale}/reference-documents`,
        },
        { label: t("pcfTracking"), path: `/${locale}/pcf-tracking` },
        {
          label: t("ccrs"),
          path: `/${locale}/ccrs`,
          tabs: [
            { label: "New CCR", path: `/${locale}/ccrs/new` },
            { label: "View All CCRs", path: `/${locale}/ccrs/view-all-ccrs` },
            { label: "Search CCRs", path: `/${locale}/ccrs/search` },
          ],
        },
      ],
    },
    { label: t("publicReviews"), path: `/${locale}/public-reviews` },
    { label: t("enquiries"), path: `/${locale}/enquiries` },
    {
      label: t("codesRnD"),
      path: `/${locale}/codes-rnd`,
      roles: [ROLES.ADMIN],
    },
    {
      label: "admin",
      path: `/${locale}/debug`,
      roles: [ROLES.ADMIN, ROLES.SYS_ADMIN],
      requireAll: false,
    },
    {
      label: t("myroles"),
      path: `/${locale}/my-roles`,
      roles: [ROLES.VIEWER],
    },
  ];

  // compute filtered navigation areas based on access
  const filteredAreas = areas.filter((area) =>
    hasAccess(area.roles, area.requireAll),
  );

  useEffect(() => {
    const index = filteredAreas.findIndex(
      (area) => pathname === area.path || pathname.startsWith(area.path + "/"),
    );
    if (index !== -1) {
      setValue(index);
    }
  }, [pathname, filteredAreas]);

  return (
    <AppBar position="sticky" sx={{ zIndex: 1000 }}>
      <Toolbar sx={{ minHeight: "48px" }}>
        <Tabs
          value={value}
          onChange={(e, newValue) => setValue(newValue)}
          aria-label="global navigation tabs"
          textColor="inherit"
          TabIndicatorProps={{ style: { backgroundColor: "#ffeb3b" } }}
        >
          {filteredAreas.map((area, index) => (
            <Tab
              key={index}
              sx={{ position: "relative", zIndex: 100 }}
              onClick={() => !area.tabs && router.push(area.path)}
              wrapped
              label={
                area.tabs ? (
                  <GlobalNavDropdown
                    index={index}
                    area={area}
                    userRoles={userRoles}
                  />
                ) : (
                  <Typography
                    sx={{
                      fontSize: "0.875rem",
                      fontWeight: 500,
                      color: "white",
                    }}
                  >
                    {area.label}
                  </Typography>
                )
              }
            />
          ))}
        </Tabs>

        {isLoggedIn ? (
          <>
            <Tooltip title="Account settings">
              <IconButton onClick={handleAvatarClick} sx={{ p: 0, ml: 2 }}>
                <Avatar
                  alt={session.user?.name || "User"}
                  src={session.user?.image || ""}
                />
              </IconButton>
            </Tooltip>
            <Menu
              anchorEl={anchorElog}
              open={Boolean(anchorElog)}
              onClose={handleMenuClose}
              anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
              transformOrigin={{ vertical: "top", horizontal: "right" }}
            >
              <MenuItem disabled>{session.user?.email}</MenuItem>
              <MenuItem
                onClick={() => {
                  signOut({ callbackUrl: "/" });
                  handleMenuClose();
                }}
              >
                <LogoutIcon fontSize="small" sx={{ mr: 1 }} />
                Sign Out
              </MenuItem>
            </Menu>
          </>
        ) : (
          <Tooltip title="Sign in to your account">
            <Button
              color="inherit"
              onClick={() => signIn("azure-ad", { callbackUrl: pathname })}
              startIcon={<LoginIcon />}
              sx={{ ml: 2 }}
            >
              Sign In
            </Button>
          </Tooltip>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default GlobalNavigation;
