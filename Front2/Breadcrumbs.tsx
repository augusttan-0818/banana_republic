'use client';

import React from 'react';
import { Breadcrumbs as MUIBreadcrumbs, Typography, Link, Box } from '@mui/material';
import { usePathname } from 'next/navigation';
import { useTranslations } from 'next-intl';

const Breadcrumbs= ({termpath}:{termpath:string}) => {
  const pathname = usePathname();
  const locale = pathname.split('/')[1]; // Extract locale
  const t = useTranslations(termpath); // Fetch translations from "navigation" namespace

  const segments = pathname
    .split('/')
    .filter(Boolean)
    .slice(1); // Remove the locale segment

  const breadcrumbs = segments.map((segment, index) => {
    const isLast = index === segments.length - 1;
    const label = t(segment);
    const path = `/${locale}/${segments.slice(0, index + 1).join('/')}`;

    return { label, path, isLast };
  });

  return (
    <Box
    sx={{
      paddingTop: "52px",
      
    }}
  >
    <MUIBreadcrumbs aria-label="breadcrumb" separator="›">
      {breadcrumbs.map((crumb, index) =>
        crumb.isLast ? (
          <Typography key={index} color="textPrimary">
            {crumb.label}
          </Typography>
        ) : (
          <Link
            key={index}
            href={crumb.path}
            style={{ textDecoration: 'none', color: 'inherit' }}
          >
            {crumb.label}
          </Link>
        )
      )}
    </MUIBreadcrumbs>
    </Box>
  );
};

export default Breadcrumbs;