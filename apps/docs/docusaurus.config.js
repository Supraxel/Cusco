// @ts-check
// Note: type annotations allow type checking and IDEs autocompletion

const lightCodeTheme = require('prism-react-renderer/themes/github');
const darkCodeTheme = require('prism-react-renderer/themes/dracula');

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'Cusco Documentation',
  tagline: 'Game agnostic modules from Supraxel',
  url: 'https://supraxel.github.com',
  baseUrl: '/cusco/',
  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'throw',
  favicon: 'img/favicon.ico',
  organizationName: 'supraxel',
  projectName: 'cusco',
  deploymentBranch: 'main',
  trailingSlash: false,

  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: require.resolve('./sidebars.js'),
        },
        blog: {
          showReadingTime: true,
        },
        theme: {
          customCss: require.resolve('./src/css/custom.css'),
        },
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      navbar: {
        title: 'Cusco',
        logo: {
          alt: ' ',
          src: 'img/logo.svg',
        },
        items: [
          {
            type: 'doc',
            docId: 'intro',
            position: 'left',
            label: 'Getting started',
          },
          // { to: '/blog', label: 'Blog', position: 'left' },
          {
            href: 'https://github.com/supraxel/cusco',
            label: 'GitHub',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Docs',
            items: [
              {
                label: 'Getting started',
                to: '/docs/intro',
              },
            ],
          },
          {
            title: 'Community',
            items: [
              {
                label: 'Discord',
                href: 'https://discord.gg/ft7yQtuRNp',
              },
              {
                label: 'Twitch',
                href: 'https://www.twitch.tv/supraxelgames',
              }
            ],
          },
          {
            title: 'More',
            items: [
              {
                label: 'GitHub',
                href: 'https://github.com/supraxel/cusco',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} Aurélien Bidon. Built with Docusaurus.`,
      },
      prism: {
        additionalLanguages: [
          'bash',
          'c', 'cpp', 'csharp',
          'docker',
          'editorconfig',
          'hcl', 'hlsl', 'http',
          'javascript', 'json',
          'makefile', 'markdown', 'mongodb',
          'regex',
          'solidity', 'sql',
          'toml', 'typescript',
          'uri',
          'yaml'
        ],
        theme: lightCodeTheme,
        darkTheme: darkCodeTheme,
      },
    }),
};

module.exports = config;
